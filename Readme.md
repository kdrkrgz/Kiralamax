<p><img src="https://imgupload.io/images/2021/04/10/kiralaMAX-Logo-resized.png" alt="Kiralamax Logo" title="Kiralamax Logo"></p>
<p><img src="https://img.shields.io/github/issues/kdrkrgz/Kiralamax" alt=""> <img src="https://img.shields.io/github/forks/kdrkrgz/Kiralamax" alt=""> <img src="https://img.shields.io/github/stars/kdrkrgz/Kiralamax" alt=""> <img src="https://img.shields.io/github/license/kdrkrgz/Kiralamax" alt=""> </p>
<p>Kiralamax, araç kiralama sektöründe faaliyet gösteren şirketlerin araç, kiralama, kullanıcı gibi bileşenlerin yönetilebilmesi ve müşterilerin online kiralama işlemlerini kısa adımlarla gerçekleştirebilmesini amaçlamaktadır. Projede OOP(Object orianted progrraming), AOP (Aspect Oriented Programming) gibi programlama modelleriyle çeşitli problemlere çözümler üretilmiştir. SOLID prensiplerine bağlı clean-code bir proje olması amaçlanmıştır.
  <br />
<h3 id="back-end">Back-end</h3>
 <br />
<p>Projenin backend tarafı .Net Core kullanılarak yazılmıştır. N-Tier Architecture kullanılarak Core, DataAccess, Entities, Business ve Api katmanlarına ayrılmıştır. Çok katmanlı mimari yapısıyla proje yönetimin kolaylaştırılması, ölçeklenebilirlik, esneklik, yeniden kullanılabilirlik ve güvenlik gibi problemlerin çözümlerinde basit ve yalın bir yol izlenmeye çalışılmıştır.</p>
 <br />
<h4 id="core-layer">Core Layer</h4>
 <br />
<p>Proje mimarisinin temel yapısnı oluşturan  kısımlarının yer aldığı katmandır.  Proje bağımlı olmayan bir katman olup plug-and-play mantığıyla geliştirilmiş modüler bir yapıya sahiptir. AOP mimarisi gereği oluşturulan  cross cutting concernler (caching, exception handling, performance, transaction gibi yapılar)  bu katmanda yer almaktadır, bu sayede uygulama genelinde (bir veya birden fazla katmanda yer alabilecek caching, validation, logging gibi yapılar) kullanılacak yapıların sistemden soyutlanıp enkapsüle edilerek istenen katmanda kullanılabilmesini sağlamaktadır. Veri erişimi için Entity framework repository soyutlamaları ve generic yapıda bu katmanda yer almaktadır, diğer bir taraftan yer alan extensionlar sayesinde metodlar genişletilerek mimariye ve ihtiyaçlara uygun hale getirilmiştir. Ayrıca katman içindeki utilities (araçlar) içinde, mail yönetimi, iş kuralları yönetimi, interceptor yönetimi, güvenlik ve IoC (Inversion of Control) gibi temel yapılar yönetilmektedir.</p>
 <br />
<h4 id="data-access-layer">Data Access Layer</h4>
 <br />
<p>Veri tabanı işlemlerinin yapıldığı katmandır, temel görevi veriye erişmek ve ekleme, silme, güncelleme gibi temel işlemlerin yerine getirilmesidir. Ayrıca projede Entity Framework kullanıldığı için data context, migrations ve entityler arasındaki mapping (db üzerindeki mappingler), annotations ve relations gibi operasyonlar yer almaktadır.</p>
 <br />
<h4 id="business-layer">Business Layer</h4>
 <br />
<p>Sunum katmanından (Api katmanı) gelen isteklerin ve verilerin işlendiği, uygulamada mantık işlemlerinin ve veri manipülasyonlarının işlendiği katmandır.  Genelde projelerde authorization işlemleri api katmanında yapılmasına karşılık olarak, bu projede iş katmanında işlenmektedir, authorization işlemlerinin bu katmanda işlenmesi ise ileride yazılacak olan bir android, ios, wearable gibi teknolojiler için geliştirilebilecek farklı arayüzlerde tekrar tekrar authorization işlemlerinin yapılmasının önüne geçmeyi hedefleyerek tek bir merkezi noktadan yönetilmesi hedeflenmiştir. İş mantıklarına göre oluşturulan servisler Autofac ile IoC conteinar&#39;a dahil edilmiştir bu sayede servisler istenen yerlerde inject (constructurdan) edilerek kullanılabilir hale gelmiştir. Asp .Net Core&#39;un sunmuş olduğu built-in dependenct injection desteği yerine Autofac teknolojisi kullanılarak nesnelerin yaşam döngülerinin hem daha yetenekli hem de daha yüksek performanslı (<a href="https://github.com/danielpalme/IocPerformance" title="Ioc performance comparison">Ioc performance comparison</a>) bir araçla kontrol edilmesi amaçlanmış ve kodda basitlik ve sadelik sağlanmıştır, bunlara ek olarak Autofac ile gelen castle dynamic proxy sayesinde AOP mimarisi çerçevesinde  metodların ve sınıfların intercept edilebilmesini sağlayan yapı geliştirilmiştir.</p>
 <br />
<h4 id="api-presentation-layer">API (Presentation) Layer</h4>
 <br />
<p>Projede kullanıcı veya arayüzle iletişimde olan kısımların tümünü içeren katmandır. Kiralamax projesinin back-end tarafı web api olup çeşitli platformlarda arayüz geliştirilebilmesine olanak sağlamaktadır, api katmanı temel olarak gelen ve giden verilerin mantıklarının sağlandığı ve controllers üzerinden belli ihtiyaçlar ve işlevler için oluşturulmuş metodlardan oluşmaktadır. Bu katmanda ayrıca verilerin maplenmesi için automapper teknolojisi kullanılmıştır bu sayede kodun basitleştirilmesi ve sadeleştirilmesi amaçlanmıştır. Ayrıca front-end tarafından gelen file dataları(statik dosyalar) bu katmanda tutulur ve bu dataların I/O işlemleri için yardımcı metodlar içermektedir. Ayrıca proje yapılandırmaları direkt olarak startup içine yazılmak yerine extension metodlarda ayrıştırılarak kodun sadeleştirilmesi amaçlanmıştır.</p>
 <br />
<h3 id="front-end">Front-end</h3>
 <br />
<p>Projenin front-end kısmı Angular (11.2.3) ile geliştirilmiş olup, Angular Material UI kullanılmıştır. Material UI sayesinde kullanışlı bir çok araç zamandan tasarruf sağlanarak projeye implemente edilebilir, ayrıca görünüm konusunda tutarlı bir tasarıma sahip olunması amaçlanmıştır. Components klasörü altında oluşturulan olaylara dayalı yapı ile modülarite ve yeniden kullanılabilirliğin artırılması amaçlanmıştır. api end pointlerine yapılan çağrıların(request) ve dönen yanıtların(response) servisler (Services) üzerinden  gerçekleştirilmesi sağlanmıştır, bu sayede hiç bir component içinde direkt api endpointine istek göndermeden servisler üzerinden iletişim sağnabilmekte ve kod tekrarının ciddi ölçüde önüne geçilerek tekrar kullanılabilirliği artırmaktadır. Models klasörü altında arayüz tarafına gelecek ve gidecek veriler modellenmiştiri bu sayede hem type safe bir yapı oluşturulması hemde kodun sadeleştirilmesi amaçlanmıştır. tüm request ve response işlemleri back-end tarafında olduğu gibi modellenmiş olup işlemin gerçekleşip gerçekleşmemesine ilişkin bilgiyi, datayı ve mesajı içermektedir, bu sayede merkezi bir bildirim ve kontrol sistemininde oluşturulması amaçlanmıştır. projede veriler pipelar ile modellenmiş ve localize edilmiştir, login guard ile ise istenen componentlerde kullanıcıların authenticate olup olmama durumlarının kontrol edilmesini sağlanması amaçlanmıştır.</p>
 <br />
<h3 id="features-zellikler-">Features (Özellikler)</h3>
<h4 id="back-end">Back-end</h4>
<ul>
<li>Rol bazlı yetkilendirme</li>
<li>Transaction yönetimi</li>
<li>Arayüzden gelen verilerin validasyonları</li>
<li>Jwt bazlı authentication ve authorization</li>
<li>Cache yönetimi</li>
<li>Log yönetimi</li>
<li>Hata Yönetimi</li>
<li>Performans yönetimi ve alarmı (operasyonlar beklenenden fazla sürerse belirlenen mail adresine otomatik bir uyarı maili atılır)</li>
<li>Statik dosya yönetimi</li>
</ul>
<h4 id="front-end">Front-end</h4>
<ul>
<li>Kiralama sistemine girmeden önce kullanıcıları karşılayan ve bilgilendiren landing sayfası</li>
<li>Dinamik tablolar, filtreler ve sayfalama</li>
<li>Rol bazlı yetkilendirme (Sadece ilgili rollere sahip kullanıcılar rolleriyle ilişkili sayfaları görebilir ve düzenleyebilir.)</li>
<li>Kullanıcı yönetimi (Admin kullanıcılar kullanıcı bilgilerini ve kullanıcı rollerini düzenlenebilir, her kullanıcı kendi bilgilerini güncelleyebilir.)</li>
<li>Kullanıcı hesabı aktivasyonu (Sisteme kayıt olunun mail adresi üzerinden hesabın aktif edilmesi için mail gönderilir.)</li>
<li>Hareketsizlik sebebiyle kullanıcı oturumunun otomatik sonlandıırlması (Kullanıcı sisteme giriş yaptıktan sonra 5 dakika hareketsiz kalırsa oturumun 30 saniye içinde sonlandırılması ve bu bildirinin sayaçla beraber kullanıcıya onay kutusu şeklinde sunulması)</li>
<li>Parola sıfırlama (Kullanıcılar mail adreslerine gelen bağlantıyı kullanarak parolalarını sıfırlayabilir.)</li>
<li>Kategori işlemleri (Kategori ekleme, silme ve düzenleme işlemleri)</li>
<li>Araç İşlemleri (Araç ekleme, silme ve düzenleme işlemleri)</li>
<li>Kiralama istatistikleri (Kiralamalar, araçlar ve mali kazançlar hakkında temel rapor)</li>
<li>İşlem onayları (kritik işlemleri tamamlarken son aşamada kullanıcı onayı alınır istenirse işlem iptal edilebilir.)</li>
<li>Kredi kartıyla veya önceden kayıt edilmiş kredi kartlarıyla ödeme yapabilme</li>
<li>Kiralama sırasında otomatik findeks puan hesabı</li>
<li>Kiralama geçmişine ulaşabilme ve tek tıkla kiralamayı sona erdirebilme</li>
<li>Kiralamalarda günlük ücretlerin ve vergi hesaplamaları</li>
</ul>
<h3 id="kurulum-ve-kullan-m">Kurulum ve Kullanım</h3>
<h5 id="back-end">Back-end</h5>
<ul>
<li><p>Backend projesini git aracını kullanarak aşağıdaki komutla bilgisayarınıza indiriniz.</p>
<p><code>git clone https://github.com/kdrkrgz/Kiralamax.git</code></p>
</li>
<li>Api katmanında yer alan appsettings.json dosyası içinden database bağlantı adresi (connection string), Jwt için gerekli token konfigürasyoları ve performans alarmları hesap  aktifleştirme ve parola sıfırlama maillerinde kullanılmak üzere gmail bilgilerini yazınız. (uygulamanın gmail ile mail atabilmesi için gmail ayarlarından <a href="https://support.google.com/accounts/answer/6010255" title="güvenliği düşük uygulamalara">güvenliği düşük uygulamalara</a> izin verilmelidir. )</li>
<li>dotnet-cli ile <code>database update</code> veya package manager console ile <code>update-database</code> komutlarıyla veri tabanı oluşturunuz. (admin hesabı ve temel roller otomatik olarak database&#39;e eklenecektir. )</li>
</ul>
 <br />
<p><strong>Admin hesap bilgileri: 
Email : admin@kiralamax.com 
Parola : 123456</strong></p>
 <br />
<h5 id="front-end">Front-end</h5>
<ul>
<li><p>Windows konsol üzerinden uygulama dizinine giderek aşağıdaki komutu çalıştırınız, bu sayede projenin çalışabilmesi için gerekli paketler indirilecektir.</p>
<p><code>npm update</code></p>
</li>
</ul>
 <br />
<h3 id="-zel-te-ekk-r">Özel Teşekkür</h3>
 <br />
<p>Eğitimde fırsat eşitliği düşüncesini savunarak, bilgilerini ve deneyimlerini herhangi bir kar amacı gütmeksizin paylaşan  ve bunun için emek veren değerli insan <a href="https://github.com/engindemirog" title="@engindemirog">@engindemirog</a> hocamıza teşekkürler ve sevgilerle..</p>

Proje Ekran Görüntüleri
<a target="_blank" href="https://ibb.co/2qn67FK"><img src="https://ibb.co/2qn67FK" /></a>
<a target="_blank" href="https://ibb.co/2qn67FK"><img src="https://ibb.co/mTxQpPx" /></a>
