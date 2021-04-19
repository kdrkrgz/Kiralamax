![](https://img.shields.io/github/issues/kdrkrgz/Kiralamax) ![](https://img.shields.io/github/forks/kdrkrgz/Kiralamax) ![](https://img.shields.io/github/stars/kdrkrgz/Kiralamax) ![](https://img.shields.io/github/license/kdrkrgz/Kiralamax) 

Kiralamax, araç kiralama sektöründe faaliyet gösteren şirketlerin araç, kiralama, kullanıcı gibi bileşenlerin yönetilebilmesi ve müşterilerin online kiralama işlemlerini kısa adımlarla gerçekleştirebilmesini amaçlamaktadır. Projede OOP(Object orianted progrraming), AOP (Aspect Oriented Programming) gibi programlama modelleriyle çeşitli sorunlara çözümler sunulmuştur. Diğer taraftan SOLID prensiplerine bağlı clean code olarak kodlanan bir proje olması amaçlanmıştır.  Proje, değerli Engin Demiroğ'un 3 ay boyunca yürütmüş olduğu yazılım geliştirici yetiştirme kampı sonunda verilen proje ödevidir.


**Bölümler**
- [Back-end](#back-end)
  * [Core Layer](#core-layer)
  * [Data Access Layer](#data-access-layer)
  * [Business Layer](#business-layer)
  * [API (Presentation) Layer](#api--presentation--layer)
- [Front-end](#front-end)
- [Tech Stack (Kullanılan Teknolojiler)](#tech-stack--kullan-lan-teknolojiler-)
- [Features (Özellikler)](#features---zellikler-)
  * [Back-end](#back-end-1)
  * [Front-end](#front-end-1)
- [Kurulum ve Kullanım](#kurulum-ve-kullan-m)
    + [Back-end](#back-end-2)
    + [Front-end](#front-end-2)
- [Özel Teşekkür](#özel-teşekkür)



### Back-end
Projenin backend tarafı .Net Core kullanılarak yazılmıştır. N-Tier Architecture kullanılarak Core, DataAccess, Entities, Business ve Api katmanlarına ayrılmıştır. Çok katmanlı mimari yapısıyla proje yönetimin kolaylaştırılması, ölçeklenebilirlik, esneklik, yeniden kullanılabilirlik ve güvenlik gibi problemlerin çözümlerinde basit ve yalın bir yol izlenmeye çalışılmıştır.

#### Core Layer
Proje mimarisinin temel yapısnı oluşturan  kısımlarının yer aldığı katmandır.  Proje bağımlı olmayan bir katman olup plug-and-play mantığıyla geliştirilmiş modüler bir yapıya sahiptir. AOP mimarisi gereği oluşturulan  cross cutting concernler (caching, exception handling, performance, transaction gibi yapılar)  bu katmanda yer almaktadır, bu sayede uygulama genelinde (bir veya birden fazla katmanda yer alabilecek caching, validation, logging gibi yapılar) kullanılacak yapıların sistemden soyutlanıp enkapsüle edilerek istenen katmanda kullanılabilmesini sağlamaktadır. Veri erişimi için Entity framework repository soyutlamaları ve generic yapıda bu katmanda yer almaktadır, diğer bir taraftan yer alan extensionlar sayesinde metodlar genişletilerek mimariye ve ihtiyaçlara uygun hale getirilmiştir. Ayrıca katman içindeki utilities (araçlar) içinde, mail yönetimi, iş kuralları yönetimi, interceptor yönetimi, güvenlik ve IoC (Inversion of Control) gibi temel yapılar yönetilmektedir.

#### Data Access Layer
Veri tabanı işlemlerinin yapıldığı katmandır, temel görevi veriye erişmek ve ekleme, silme, güncelleme gibi temel işlemlerin yerine getirilmesidir. Ayrıca projede Entity Framework kullanıldığı için data context, migrations ve entityler arasındaki mapping (db üzerindeki mappingler), annotations ve relations gibi operasyonlar yer almaktadır.

#### Business Layer
Sunum katmanından (Api katmanı) gelen isteklerin ve verilerin işlendiği, uygulamada mantık işlemlerinin ve veri manipülasyonlarının işlendiği katmandır.  Genelde projelerde authorization işlemleri api katmanında yapılmasına karşılık olarak, bu projede iş katmanında işlenmektedir, authorization işlemlerinin bu katmanda işlenmesi ise ileride yazılacak olan bir android, ios, wearable gibi teknolojiler için geliştirilebilecek farklı arayüzlerde tekrar tekrar authorization işlemlerinin yapılmasının önüne geçmeyi hedefleyerek tek bir merkezi noktadan yönetilmesi hedeflenmiştir. İş mantıklarına göre oluşturulan servisler Autofac ile IoC conteinar'a dahil edilmiştir bu sayede servisler istenen yerlerde inject (constructurdan) edilerek kullanılabilir hale gelmiştir. Asp .Net Core'un sunmuş olduğu built-in dependenct injection desteği yerine Autofac teknolojisi kullanılarak nesnelerin yaşam döngülerinin hem daha yetenekli hem de daha yüksek performanslı ([Ioc performance comparison](https://github.com/danielpalme/IocPerformance "Ioc performance comparison")) bir araçla kontrol edilmesi amaçlanmış ve kodda basitlik ve sadelik sağlanmıştır, bunlara ek olarak Autofac ile gelen castle dynamic proxy sayesinde AOP mimarisi çerçevesinde  metodların ve sınıfların intercept edilebilmesini sağlayan yapı geliştirilmiştir.

#### API (Presentation) Layer
Projede kullanıcı veya arayüzle iletişimde olan kısımların tümünü içeren katmandır. Kiralamax projesinin back-end tarafı web api olup çeşitli platformlarda arayüz geliştirilebilmesine olanak sağlamaktadır, api katmanı temel olarak gelen ve giden verilerin mantıklarının sağlandığı ve controllers üzerinden belli ihtiyaçlar ve işlevler için oluşturulmuş metodlardan oluşmaktadır. Bu katmanda ayrıca verilerin maplenmesi için automapper teknolojisi kullanılmıştır bu sayede kodun basitleştirilmesi ve sadeleştirilmesi amaçlanmıştır. Ayrıca front-end tarafından gelen file dataları(statik dosyalar) bu katmanda tutulur ve bu dataların I/O işlemleri için yardımcı metodlar içermektedir. Ayrıca proje yapılandırmaları direkt olarak startup içine yazılmak yerine extension metodlarda ayrıştırılarak kodun sadeleştirilmesi amaçlanmıştır.


### Front-end
Projenin front-end kısmı Angular (11.2.3) ile geliştirilmiş olup, Angular Material UI kullanılmıştır. Material UI sayesinde kullanışlı bir çok araç zamandan tasarruf sağlanarak projeye implemente edilebilir, ayrıca görünüm konusunda tutarlı bir tasarıma sahip olunması amaçlanmıştır. Components klasörü altında oluşturulan olaylara dayalı yapı ile modülarite ve yeniden kullanılabilirliğin artırılması amaçlanmıştır. api end pointlerine yapılan çağrıların(request) ve dönen yanıtların(response) servisler (Services) üzerinden  gerçekleştirilmesi sağlanmıştır, bu sayede hiç bir component içinde direkt api endpointine istek göndermeden servisler üzerinden iletişim sağnabilmekte ve kod tekrarının ciddi ölçüde önüne geçilerek tekrar kullanılabilirliği artırmaktadır. Models klasörü altında arayüz tarafına gelecek ve gidecek veriler modellenmiştiri bu sayede hem type safe bir yapı oluşturulması hemde kodun sadeleştirilmesi amaçlanmıştır. tüm request ve response işlemleri back-end tarafında olduğu gibi modellenmiş olup işlemin gerçekleşip gerçekleşmemesine ilişkin bilgiyi, datayı ve mesajı içermektedir, bu sayede merkezi bir bildirim ve kontrol sistemininde oluşturulması amaçlanmıştır. projede veriler pipelar ile modellenmiş ve localize edilmiştir, login guard ile ise istenen componentlerde kullanıcıların authenticate olup olmama durumlarının kontrol edilmesini sağlanması amaçlanmıştır.

### Tech Stack (Kullanılan Teknolojiler)

| Back-end  | Front-end |
| ------------- | ------------- |
	@@ -42,8 +56,8 @@ Projenin front-end kısmı Angular (11.2.3) ile geliştirilmiş olup, Angular Ma
| Swashbuckle (Swagger)  |
| Fluent Validation  |

### Features (Özellikler)
#### Back-end
+ Rol bazlı yetkilendirme
+ Transaction yönetimi
+ Arayüzden gelen verilerin validasyonları
	@@ -54,7 +68,7 @@ Projenin front-end kısmı Angular (11.2.3) ile geliştirilmiş olup, Angular Ma
+ Performans yönetimi ve alarmı (operasyonlar beklenenden fazla sürerse belirlenen mail adresine otomatik bir uyarı maili atılır)
+ Statik dosya yönetimi

#### Front-end
+ Kiralama sistemine girmeden önce kullanıcıları karşılayan ve bilgilendiren landing sayfası
+ Dinamik tablolar, filtreler ve sayfalama
+ Rol bazlı yetkilendirme (Sadece ilgili rollere sahip kullanıcılar rolleriyle ilişkili sayfaları görebilir ve düzenleyebilir.)
	@@ -71,27 +85,25 @@ Projenin front-end kısmı Angular (11.2.3) ile geliştirilmiş olup, Angular Ma
+ Kiralama geçmişine ulaşabilme ve tek tıkla kiralamayı sona erdirebilme
+ Kiralamalarda günlük ücretlerin ve vergi hesaplamaları


### Kurulum ve Kullanım
##### Back-end
+ Backend projesini git aracını kullanarak aşağıdaki komutla bilgisayarınıza indiriniz.

  `git clone https://github.com/kdrkrgz/Kiralamax.git`
+ Api katmanında yer alan appsettings.json dosyası içinden database bağlantı adresi (connection string), Jwt için gerekli token konfigürasyoları ve performans alarmları hesap  aktifleştirme ve parola sıfırlama maillerinde kullanılmak üzere gmail bilgilerini yazınız. (uygulamanın gmail ile mail atabilmesi için gmail ayarlarından [güvenliği düşük uygulamalara](https://support.google.com/accounts/answer/6010255 "güvenliği düşük uygulamalara") izin verilmelidir. )
+ dotnet-cli ile `database update` veya package manager console ile `update-database` komutlarıyla veri tabanı oluşturunuz. (admin hesabı ve temel roller otomatik olarak database'e eklenecektir. )

**Admin hesap bilgileri: 
Email : admin@kiralamax.com 
Parola : 123456**

##### Front-end
+ Windows konsol üzerinden uygulama dizinine giderek aşağıdaki komutu çalıştırınız, bu sayede projenin çalışabilmesi için gerekli paketler indirilecektir.

  `npm update
`


### Özel Teşekkür
Eğitimde fırsat eşitliği düşüncesini savunarak, bilgilerini ve deneyimlerini herhangi bir kar amacı gütmeksizin paylaşan  ve bunun için emek veren değerli insan [@engindemirog](https://github.com/engindemirog "@engindemirog") hocamıza teşekkürler ve sevgilerle..
